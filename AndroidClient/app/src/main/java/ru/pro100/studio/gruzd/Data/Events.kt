package ru.pro100.studio.gruzd.Data

import java.util.*


data class Events(
    var base64Pics: Array<String> = emptyArray(),
    var number: String = "",
    var occured: Date = Date(),
    var auxData: String = "",
    var classifiedType: Int = 0,
    var weight: Int = 0,
    var unloadingZoneId: Int = 0
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