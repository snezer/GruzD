package ru.pro100.studio.gruzd

import android.content.Intent
import androidx.appcompat.app.AppCompatActivity
import android.os.Bundle
import android.speech.tts.TextToSpeech
import android.util.Log
import android.widget.ListView
import android.widget.SimpleAdapter
import androidx.appcompat.widget.LinearLayoutCompat
import com.google.android.material.floatingactionbutton.FloatingActionButton
import ru.pro100.studio.gruzd.Data.QualityTestResult
import java.util.*
import kotlin.collections.HashMap

class TestResultActivity : AppCompatActivity() {
    val TAG: String = "TestResultActivity"

    lateinit var podsAdapter: SimpleAdapter


    val pods = mutableListOf<HashMap<String, String>>(
            HashMap<String, String>().apply {
                put("Zone", "ЗонаA")
                put("Uuid", "7778647")
                put("TestResult", "Брак товара")
            },
            HashMap<String, String>().apply {
                put("Zone", "Зона Б")
                put("Uuid", "65796745")
                put("TestResult", "Несоответствие номер")
            },
            HashMap<String, String>().apply {
                put("Zone", "Зона А")
                put("Uuid", "4775858")
                put("TestResult", "Товар качественный")
            })

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_test_result)

        Init()

    }

    fun Init(){
        val podsList: ListView = findViewById(R.id.testPodsList)
        podsAdapter = SimpleAdapter(
            applicationContext,
            pods,
            R.layout.item_pod,
            arrayOf("Zone", "Uuid",  "TestResult"),
            intArrayOf(R.id.zone, R.id.uuid, R.id.testResult)
        )
        podsList.adapter = podsAdapter
        podsList.setOnItemClickListener{ parent, view, position, id ->
            Log.d(TAG, pods[position]["Uuid"].toString())
        }

        var floatingButton = findViewById<FloatingActionButton>(R.id.new_card_button)
        floatingButton.setOnClickListener{
            val intent = Intent(this, ShipmentCard::class.java)
            startActivity(intent)
        }

    }
}