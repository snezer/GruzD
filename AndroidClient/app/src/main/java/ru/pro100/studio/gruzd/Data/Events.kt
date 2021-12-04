package ru.pro100.studio.gruzd.Data

import com.google.gson.annotations.SerializedName
import java.util.*


data class Events(
    @SerializedName("base64Pics") var base64Pics: Array<String> = emptyArray(),
    @SerializedName("number")var number: String = "",
    @SerializedName("occured")var occured: Date = Date(),
    @SerializedName("auxData")var auxData: String = "",
    @SerializedName("classifiedType")var classifiedType: Int = 0,
    @SerializedName("weight")var weight: Int = 0,
    @SerializedName("unloadingZoneId:")var unloadingZoneId: Int = 0
) {
    val photoFileName
        get() = "IMG_@Date.jpg"

    override fun equals(other: Any?): Boolean {
        if (this === other) return true
        if (javaClass != other?.javaClass) return false

        other as Events

        if (!base64Pics.contentEquals(other.base64Pics)) return false

        return true
    }

    override fun hashCode(): Int {
        return base64Pics.contentHashCode()
    }
}