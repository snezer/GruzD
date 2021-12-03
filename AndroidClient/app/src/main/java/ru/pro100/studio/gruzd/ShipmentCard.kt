package ru.pro100.studio.gruzd

import android.content.Intent
import android.content.pm.PackageManager
import android.content.pm.ResolveInfo
import android.net.Uri
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.provider.MediaStore
import android.widget.ImageView
import androidx.core.content.FileProvider
import com.google.android.material.textfield.TextInputEditText
import ru.pro100.studio.gruzd.Data.Events
import ru.pro100.studio.gruzd.ViewModels.FileViewModel
import java.io.File

class ShipmentCard : AppCompatActivity() {
    companion object{
        private const val REQUEST_PHOTO = 2
    }

    lateinit var photoView: ImageView
    lateinit var imgButton: ImageView
    lateinit var tiZoneEdit: TextInputEditText
    lateinit var tiWagon: TextInputEditText
    lateinit var tiUuid: TextInputEditText
    lateinit var tiScanResult: TextInputEditText
    private lateinit var photoFile: File
    private lateinit var events: Events
    private lateinit var fileDetailViewModel: FileViewModel
    private lateinit var photoUri: Uri


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
            }
        }
    }

    fun init(){
        imgButton = findViewById(R.id.btnImageView)
        photoView = findViewById(R.id.foto_view)
        tiZoneEdit = findViewById(R.id.tvZone)
        tiWagon = findViewById(R.id.tvWagon)
        tiUuid = findViewById(R.id.itUuid)
        tiScanResult = findViewById(R.id.scanResult)
        events = Events()
        fileDetailViewModel = FileViewModel()
        photoFile = fileDetailViewModel.getPhotoFile(events)
        photoUri = FileProvider.getUriForFile(this, "ru.pro100.studio.gruzd.fileprovider", photoFile)
    }


}