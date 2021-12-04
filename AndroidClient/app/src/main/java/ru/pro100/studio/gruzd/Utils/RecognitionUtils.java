package ru.pro100.studio.gruzd.Utils;

import android.content.Context;
import android.graphics.Bitmap;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;

import org.pytorch.IValue;
import org.pytorch.Module;
import org.pytorch.Tensor;
import org.pytorch.torchvision.TensorImageUtils;

import java.io.File;
import java.io.IOException;

import java.io.FileOutputStream;
import java.io.InputStream;
import java.io.OutputStream;



public class RecognitionUtils {


    private Module module = null;

    public static String assetFilePath(Context context, String assetName) throws IOException{
        File file = new File(context.getFilesDir(), assetName);
        if (file.exists() && file.length() > 0){
            file.getAbsolutePath();
        }
        try(InputStream is = context.getAssets().open(assetName)){
            try(OutputStream os = new FileOutputStream(file)){
                byte[] buffer = new byte[4 * 1024];
                int read;
                while ((read = is.read(buffer)) != -1){
                    os.write(buffer, 0, read);
                }
                os.flush();
            }
            return file.getAbsolutePath();
        }
    }

    public void StartRecognition(Context context, Bitmap bitmap, ImageView imageView, TextView textView){
        try {
            module = Module.load(assetFilePath(context, "best.torchscript.ptl"));
        } catch (IOException e) {
            Log.e("ASSET-LOAD", "Error loading asset");
            return;
        }

        //Show image on UI
        imageView.setImageBitmap(bitmap);

        final Tensor inputTensor = TensorImageUtils.bitmapToFloat32Tensor(bitmap,
                TensorImageUtils.TORCHVISION_NORM_MEAN_RGB, TensorImageUtils.TORCHVISION_NORM_STD_RGB);
        //running the model
        final Tensor outputTensor = module.forward(IValue.from(inputTensor)).toTensor();

        //getting tensor countent as java array of floats
        final float[] scores = outputTensor.getDataAsFloatArray();

        //searching for the index with maximum score
        float maxScore = -Float.MAX_VALUE;
        int maxScoreIdx = -1;
        for(int i = 0; i < scores.length; i++){
            if(scores[i] > maxScore){
                maxScore = scores[i];
                maxScoreIdx = i;
            }
        }

        String className = com.example.ptmobilewalkthru.ImageNetClasses.IMAGENET_CLASSES[maxScoreIdx];
        textView.setText(className);
    }
}
