using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class Init : MonoBehaviour
{

    private const int KeyLength = 32;
    private const string Charset = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

    private void Awake()
    {
        string user_id = PlayerPrefs.GetString("user_id");
    
        if (string.IsNullOrEmpty(user_id))
        {
            PlayerPrefs.SetString("user_id", GenerateUserID());
        }
        Debug.Log("user_id: " + PlayerPrefs.GetString("user_id", GenerateUserID()));
    }


    private string GenerateUserID()
    {
        StringBuilder stringBuilder = new StringBuilder(KeyLength);
        System.Random random = new System.Random();

        for (int i = 0; i < KeyLength; i++)
        {
            int index = random.Next(Charset.Length);
            char randomChar = Charset[index];
            stringBuilder.Append(randomChar);
        }

        return stringBuilder.ToString();
    }
}
