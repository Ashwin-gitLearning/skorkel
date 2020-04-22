using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

/// <summary>
/// Summary description for CryptoGraphy
/// </summary>
public class CryptoGraphy
{
    public CryptoGraphy()
    {
        //
        // TODO: Add constructor logic here
        //

    }

    public string Encrypt(string toEncrypt)
    {

        byte[] keyArray;

        byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

        string key = ")(*&";

        MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();

        keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));

        //Always release the resources and flush data of the Cryptographic service provide. Best Practice

        hashmd5.Clear();

        TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();

        //set the secret key for the tripleDES algorithm

        tdes.Key = keyArray;

        //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)

        tdes.Mode = CipherMode.ECB;

        //padding mode(if any extra byte added)




        tdes.Padding = PaddingMode.PKCS7;




        ICryptoTransform cTransform = tdes.CreateEncryptor();

        //transform the specified region of bytes array to resultArray

        byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);

        //Release resources held by TripleDes Encryptor

        tdes.Clear();

        //Return the encrypted data into unreadable string format

        return Convert.ToBase64String(resultArray, 0, resultArray.Length);

    }

    public string Decrypt(string cipherString)
    {
        try
        {
            cipherString = cipherString.Replace(" ", "+");
            byte[] keyArray;
            //get the byte code of the string
            byte[] toEncryptArray = Convert.FromBase64String(cipherString);
            string key = ")(*&";
            //if hashing was used get the hash code with regards to your key
            MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
            //release any resource held by the MD5CryptoServiceProvider
            hashmd5.Clear();
            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray;
            try
            {
                resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            }
            catch
            {
                return "";
            }
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        catch
        {
            return "";
        }
    }

}