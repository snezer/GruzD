package ru.pro100.studio.gruzd.api

import android.provider.CalendarContract
import retrofit2.Call
import retrofit2.Callback
import retrofit2.Response
import retrofit2.Retrofit
import ru.pro100.studio.gruzd.Data.Events
import ru.pro100.studio.gruzd.Services.ServiceBuilder

class GruzdApiService {
    fun addEvent(eventData: Events, onResult: (Events?) -> Unit){
        val retrofit = ServiceBuilder.buildService(GruzdApi::class.java)
        retrofit.pullEvent(eventData).enqueue(
            object : Callback<Events> {
                override fun onFailure(call: Call<Events>, t: Throwable) {
                    onResult(null)
                }
                override fun onResponse( call: Call<Events>, response: Response<Events>) {
                    val addedEvent = response.body()
                    onResult(addedEvent)
                }
            }
        )
    }
}