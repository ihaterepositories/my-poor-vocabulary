using System;
using System.Globalization;
using Constants;
using Modules.SchoolSystem.StudentProgression.Models;
using Modules.SchoolSystem.StudentProgression.Requests;
using UnityEngine;
using Utils.ResponseModels;

namespace Modules.SchoolSystem.StudentProgression
{
    public class StudentProgressUpdater : MonoBehaviour
    {
        private void Start()
        {
            SendUpdateProgressRequest();
        }

        private void SendUpdateProgressRequest()
        {
            // If student has never been logined in school system than stop progress update process
            if (!PlayerPrefs.HasKey(AppPlayerPrefsKeys.SchoolSystemStudentIdKey))
            {
                Debug.LogWarning("Cannot update student activity, no School System Student Id Key, student is not logged in.");
                return;
            }

            var updateStudentProgressModel = new UpdateStudentProgressModel()
            {
                StudentId = Guid.Parse(PlayerPrefs.GetString(AppPlayerPrefsKeys.SchoolSystemStudentIdKey)),
                GamesCompleted = PlayerPrefs.GetInt(AppPlayerPrefsKeys.PlayedMiniGamesCountKey, 0),
                AverageScorePerGame = PlayerPrefs.GetFloat(AppPlayerPrefsKeys.AverageMiniGamesScoreKey, 0f),
                WordsCountInVocabulary = PlayerPrefs.GetInt(AppPlayerPrefsKeys.VocabularyWordsCountKey, 0),
                LastVocabularyUpdate = DateTime.TryParse(
                    PlayerPrefs.GetString(AppPlayerPrefsKeys.LastVocabularyUpdateDateKey, string.Empty),
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out var parsedDate
                ) ? parsedDate : DateTime.UtcNow
            };
            
            StartCoroutine(
                UpdateStudentProgressRequestHandler
                    .SendUpdateProgressRequest(updateStudentProgressModel, ShowRequestResultToConsole));
        }

        private void ShowRequestResultToConsole(NoReturnDataResponse response)
        {
            Debug.Log("Student progress request executed with response message: " + response.ErrorDescription);
        }
    }
}