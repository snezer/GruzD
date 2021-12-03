package ru.pro100.studio.gruzd

import android.media.Image
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.os.ParcelUuid
import android.widget.ImageView
import com.google.android.material.textfield.TextInputEditText

class ShipmentCard : AppCompatActivity() {

    lateinit var photoView: ImageView
    lateinit var imgButton: ImageView
    lateinit var tiZoneEdit: TextInputEditText
    lateinit var tiWagon: TextInputEditText
    lateinit var tiUuid: TextInputEditText
    lateinit var tiScanResult: TextInputEditText

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
    }
}