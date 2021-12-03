package ru.pro100.studio.gruzd

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.util.Log
import android.widget.Button
import androidx.appcompat.widget.AppCompatButton
import ru.pro100.studio.gruzd.Data.QualityTestResult
import java.util.*
import kotlin.math.log

class MainActivity : AppCompatActivity() {
    val TAG: String = "MainActivity"

    lateinit var btnLogin: AppCompatButton

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        Init()
    }

    fun Init(){
        btnLogin = findViewById(R.id.btnLogin)
        btnLogin.setOnClickListener {
            val intent = Intent(this, TestResultActivity::class.java)
            startActivity(intent)
        }
    }
}