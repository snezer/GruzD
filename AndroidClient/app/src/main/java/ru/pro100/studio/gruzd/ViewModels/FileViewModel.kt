package ru.pro100.studio.gruzd.ViewModels

import androidx.lifecycle.ViewModel
import ru.pro100.studio.gruzd.Data.Events
import ru.pro100.studio.gruzd.Repository.FileRepository
import java.io.File

class FileViewModel: ViewModel() {
    private val fileRepository = FileRepository.get()

    fun getPhotoFile(events: Events): File{
        return fileRepository.getPhotoFile(events)
    }
}