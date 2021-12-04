package ru.pro100.studio.gruzd.api


import retrofit2.Call
import retrofit2.http.Body
import retrofit2.http.Headers
import retrofit2.http.POST
import ru.pro100.studio.gruzd.Data.Events

interface GruzdApi{

    @Headers("Content-Type: application/json")
    @POST("/api/v1/Events")
    fun pullEvent(@Body event: Events): Call<Events>
}