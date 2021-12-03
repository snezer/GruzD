package ru.pro100.studio.gruzd

import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.widget.ImageView
import com.google.android.material.textfield.TextInputEditText
import ru.pro100.studio.gruzd.Data.Events
import ru.pro100.studio.gruzd.ViewModels.FileViewModel
import java.io.File

class ShipmentCard : AppCompatActivity() {

    lateinit var photoView: ImageView
    lateinit var imgButton: ImageView
    lateinit var tiZoneEdit: TextInputEditText
    lateinit var tiWagon: TextInputEditText
    lateinit var tiUuid: TextInputEditText
    lateinit var tiScanResult: TextInputEditText
    private lateinit var photoFile: File
    private lateinit var events: Events
    private lateinit var fileDetailViewModel: FileViewModel


    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_shipment_card)

        init()
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
    }


}