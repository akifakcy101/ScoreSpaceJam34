using UnityEngine;
using LootLocker.Requests;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class Scoreboard : MonoBehaviour
{
    public TMP_InputField memberIDInput, scoreInput;
    public int leaderboardID;
    public TextMeshProUGUI[] entries;

    int maxScore = 100;

    void Start()
    {
        // Oyuncunun nickname'ini al ya da rastgele oluştur
        string playerID = PlayerPrefs.GetString("playerID", "");
        if (string.IsNullOrEmpty(playerID))
        {
            playerID = "Player_" + UnityEngine.Random.Range(1000, 9999);
            PlayerPrefs.SetString("playerID", playerID);
        }

        LootLockerSDKManager.StartGuestSession(playerID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("LootLocker oturumu açıldı: " + playerID);
                ShowScores();
            }
            else
            {
                Debug.LogError("Oturum açılamadı: " + response.errorData.message);
            }
        });
    }

    public static void SubmitScoreStatic(string memberID, int newScore, int leaderboardID)
    {
        LootLockerSDKManager.GetMemberRank(leaderboardID.ToString(), memberID, (response) =>
        {
            if (response.success)
            {
                int currentScore = response.score;

                if (newScore > currentScore)
                {
                    LootLockerSDKManager.SubmitScore(memberID, newScore, leaderboardID.ToString(), (submitResponse) =>
                    {
                        if (submitResponse.success)
                            Debug.Log("Skor güncellendi!");
                        else
                            Debug.LogError("Skor gönderilemedi: " + submitResponse.errorData.message);
                    });
                }
                else
                {
                    Debug.Log("Yeni skor daha düşük veya eşit.");
                }
            }
            else
            {
                LootLockerSDKManager.SubmitScore(memberID, newScore, leaderboardID.ToString(), (submitResponse) =>
                {
                    if (submitResponse.success)
                        Debug.Log("Skor gönderildi (ilk kez).");
                    else
                        Debug.LogError("Skor gönderilemedi: " + submitResponse.errorData.message);
                });
            }
        });
    }
    /*public void SubmitScore()
    {
        string memberID = memberIDInput.text;
        int newScore;

        if (string.IsNullOrEmpty(memberID) || !int.TryParse(scoreInput.text, out newScore))
        {
            Debug.LogWarning("Geçerli isim ve sayı gir!");
            return;
        }

        // Önce bu oyuncunun önceki skorunu kontrol et
        LootLockerSDKManager.GetMemberRank(leaderboardID.ToString(), memberID, (response) =>
        {
            if (response.success)
            {
                int currentScore = response.score;

                Debug.Log($"Mevcut skor: {currentScore}, Yeni skor: {newScore}");

                if (newScore > currentScore)
                {
                    // Yeni skor daha yüksekse güncelle
                    LootLockerSDKManager.SubmitScore(memberID, newScore, leaderboardID.ToString(), (submitResponse) =>
                    {
                        if (submitResponse.success)
                        {
                            Debug.Log("Skor güncellendi!");
                            ShowScores();
                        }
                        else
                        {
                            Debug.LogError("Skor gönderilemedi: " + submitResponse.errorData.message);
                        }
                    });
                }
                else
                {
                    Debug.Log("Yeni skor mevcut skordan düşük veya eşit, gönderilmedi.");
                }
            }
            else
            {
                // Oyuncunun hiç skoru yoksa → doğrudan ekle
                Debug.Log("Oyuncunun skoru bulunamadı, ilk kez gönderiliyor.");

                LootLockerSDKManager.SubmitScore(memberID, newScore, leaderboardID.ToString(), (submitResponse) =>
                {
                    if (submitResponse.success)
                    {
                        Debug.Log("Skor gönderildi (ilk kez).");
                        ShowScores();
                    }
                    else
                    {
                        Debug.LogError("Skor gönderilemedi: " + submitResponse.errorData.message);
                    }
                });
            }
        });
    }*/

    public void ShowScores()
    {
        LootLockerSDKManager.GetScoreList(leaderboardID.ToString(), maxScore, (response) =>
        {
            if (response.success)
            {
                var scores = response.items;

                if (scores.Length == 0)
                {
                    // Hiç skor yoksa listeyi boş göster
                    for (int i = 0; i < entries.Length; i++)
                    {
                        entries[i].text = $"{i + 1}. ---";
                    }
                    Debug.Log("Henüz hiç skor gönderilmemiş.");
                    return;
                }

                for (int i = 0; i < entries.Length; i++)
                {
                    if (i < scores.Length)
                    {
                        entries[i].text = $"{scores[i].rank}. {scores[i].member_id} - {scores[i].score}";
                    }
                    else
                    {
                        entries[i].text = $"{i + 1}. ---";
                    }
                }
            }
            else
            {
                Debug.LogError("Skorlar çekilemedi: " + response.errorData.message);
            }
        });
    }
}
