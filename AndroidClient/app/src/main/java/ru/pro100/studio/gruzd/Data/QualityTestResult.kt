package ru.pro100.studio.gruzd.Data


import android.os.Parcel
import android.os.Parcelable
import java.text.SimpleDateFormat
import java.util.*

data class QualityTestResult(var id: UUID = UUID.randomUUID(),
                             var zone: String = "",
                             var testResult: String = "")