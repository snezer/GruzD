package ru.pro100.studio.gruzd

import android.app.Activity
import android.content.Intent
import android.content.pm.PackageManager
import android.content.pm.ResolveInfo
import android.graphics.Bitmap
import android.graphics.Point
import android.graphics.drawable.BitmapDrawable
import android.net.Uri
import android.os.Build
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.MediaStore
import android.widget.ImageView
import androidx.core.content.FileProvider
import com.google.android.material.textfield.TextInputEditText
import retrofit2.Call
import retrofit2.Retrofit
import ru.pro100.studio.gruzd.Data.Events
import ru.pro100.studio.gruzd.Utils.PictureUtils
import ru.pro100.studio.gruzd.ViewModels.FileViewModel
import ru.pro100.studio.gruzd.api.GruzdApi
import ru.pro100.studio.gruzd.api.GruzdApiService
import java.io.File
import android.view.View.MeasureSpec
import android.widget.Toast
import androidx.annotation.RequiresApi
import ru.pro100.studio.gruzd.Utils.RecognitionUtils
import java.io.ByteArrayOutputStream
import java.time.LocalDateTime
import java.time.ZoneId
import java.util.*


class ShipmentCard : AppCompatActivity() {
    companion object{
        private const val REQUEST_PHOTO = 2
    }

    lateinit var photoView: ImageView
    lateinit var imgButton: ImageView
    lateinit var imgSendButton: ImageView
    lateinit var tiZoneEdit: TextInputEditText
    lateinit var tiUuid: TextInputEditText
    lateinit var tiScanResult: TextInputEditText
    private lateinit var photoFile: File
    private lateinit var events: Events
    private lateinit var fileDetailViewModel: FileViewModel
    private lateinit var photoUri: Uri
    private lateinit var picUtil: PictureUtils
    private lateinit var retroFit: Retrofit
    private lateinit var gruzdApi: GruzdApi
    private lateinit var recogUtils: RecognitionUtils

    fun updatePhotoView() {
        if (photoFile.exists()) {
            val bitmap = picUtil.getScaledBitmap(photoFile.path, this)
            photoView.setImageBitmap(bitmap)
        } else {
            photoView.setImageDrawable(null)
        }
    }

    @RequiresApi(Build.VERSION_CODES.O)
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_shipment_card)

        init()

        imgButton.apply {
            val packageManager: PackageManager = this.context.packageManager

            val captureImage = Intent(MediaStore.ACTION_IMAGE_CAPTURE)
            val resolvedActivity: ResolveInfo? = packageManager.resolveActivity(captureImage, PackageManager.MATCH_DEFAULT_ONLY)
            if (resolvedActivity == null){
                isEnabled = false
            }

            setOnClickListener {
                captureImage.putExtra(MediaStore.EXTRA_OUTPUT, photoUri)

                val cameraActivities: List<ResolveInfo> =
                    packageManager.queryIntentActivities(captureImage, PackageManager.MATCH_DEFAULT_ONLY)
                for (cameraActivity in cameraActivities) {
                        this.context.grantUriPermission(
                        cameraActivity.activityInfo.packageName,
                        photoUri,
                        Intent.FLAG_GRANT_WRITE_URI_PERMISSION)
                }
                startActivityForResult(captureImage, REQUEST_PHOTO)
                updatePhotoView()
                val bitmap: Bitmap = Bitmap.createBitmap(photoView.getDrawingCache())

                recogUtils.StartRecognition(this@ShipmentCard, bitmap, photoView, tiUuid)
            }
        }
        imgSendButton.setOnClickListener{

            photoView.setDrawingCacheEnabled(true)
            photoView.measure(
                MeasureSpec.makeMeasureSpec(0, MeasureSpec.UNSPECIFIED),
                MeasureSpec.makeMeasureSpec(0, MeasureSpec.UNSPECIFIED)
            )
            photoView.layout(
                0, 0,
                photoView.getMeasuredWidth(), photoView.getMeasuredHeight()
            )
            photoView.buildDrawingCache(true)
            val bitmap: Bitmap = Bitmap.createBitmap(photoView.getDrawingCache())
            photoView.setDrawingCacheEnabled(false)

            val stream = ByteArrayOutputStream()

            bitmap.compress(Bitmap.CompressFormat.JPEG, 90, stream)

            val image = stream.toByteArray()

            val base64Encoded: String? = Base64.getEncoder().encodeToString(image)

            var apiService = GruzdApiService()
            val eventInfo = Events(auxData = "",
                                 base64Pics = if (base64Encoded != null) arrayOf(base64Encoded) else emptyArray(),
                                 classifiedType = 0,
                                 number = tiUuid.text.toString(),
                                 occured = Date.from((LocalDateTime.now().atZone(ZoneId.systemDefault())).toInstant()),
                                 unloadingZoneId = 1,
                                 weight = 100)
            apiService.addEvent(eventInfo){
                if (it != null){
                    val toast = Toast.makeText(applicationContext, "Данные успешно отправлены", Toast.LENGTH_SHORT)
                }else{

                }
            }
        }
    }


    override fun onActivityResult(requestCode: Int,
                                  resultCode: Int,
                                  data: Intent?) {
        super.onActivityResult(requestCode, resultCode, data)
        if (resultCode != Activity.RESULT_OK) {
            return
        }
        updatePhotoView()
    }

    fun init(){

        imgButton = findViewById(R.id.btnImageView)
        imgSendButton = findViewById(R.id.btnSendImageView)
        photoView = findViewById(R.id.foto_view)
        tiZoneEdit = findViewById(R.id.tvZone)
        tiUuid = findViewById(R.id.itUuid)
        tiScanResult = findViewById(R.id.scanResult)
        events = Events()
        fileDetailViewModel = FileViewModel()
        photoFile = fileDetailViewModel.getPhotoFile(events)
        photoUri = FileProvider.getUriForFile(this, "ru.pro100.studio.gruzd.fileprovider", photoFile)
        picUtil = PictureUtils()
        retroFit = Retrofit.Builder().baseUrl("http://192.168.1.50:5000").build()
        gruzdApi = retroFit.create(GruzdApi::class.java)
        recogUtils = RecognitionUtils()
    }
}
