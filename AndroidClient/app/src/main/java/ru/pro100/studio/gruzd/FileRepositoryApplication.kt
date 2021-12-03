package ru.pro100.studio.gruzd

import android.app.Application
import ru.pro100.studio.gruzd.Repository.FileRepository

class FileRepositoryApplication: Application() {
    override fun onCreate() {
        super.onCreate()
        FileRepository.initialize(this)
    }
}