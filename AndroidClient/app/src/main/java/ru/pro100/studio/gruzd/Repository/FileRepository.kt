package ru.pro100.studio.gruzd.Repository

import android.content.Context
import android.os.Build
import ru.pro100.studio.gruzd.Data.Events
import java.io.File
import java.util.concurrent.Executors
import kotlin.IllegalStateException

class FileRepository private constructor(context: Context){
    private val executor = Executors.newSingleThreadExecutor()
    private val filesDir = context.applicationContext.filesDir

    companion object {
        private  var INSTANCE: FileRepository? = null

        fun initialize(context: Context){
            if (INSTANCE == null){
                INSTANCE = FileRepository(context)
            }
        }
        fun get(): FileRepository{
            return INSTANCE ?:
            throw IllegalStateException("FileRepository must be initialized")
        }

    }

    fun getPhotoFile(events: Events): File = File (filesDir, events.photoFileName)


}