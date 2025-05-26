using Modules.MiniGames.CorrectionGame.Data.Generation.TypoGenerators;
using Modules.MiniGames.CorrectionGame.Data.Generation.TypoGenerators.Interfaces;
using Modules.MiniGames.PasteGame.Data.Generation.SentenceGenerators;
using Modules.MiniGames.PasteGame.Data.Generation.SentenceGenerators.Interfaces;
using Modules.PersonalVocabulary;
using Modules.PersonalVocabulary.Data.Storage.Interfaces;
using Modules.PersonalVocabulary.Data.Storage.Services;
using Modules.ScoreModule;
using Navigation;
using Navigation.Effects;
using UnityEngine;
using UserInterface.Sound;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameObject vocabularyControllerPrefab;
        [SerializeField] private GameObject fogEffectPrefab;
        [SerializeField] private GameObject scoreControllerPrefab;
        [SerializeField] private GameObject eventSoundPlayerPrefab;
        
        public override void InstallBindings()
        {
            BindSingletons();
            BindDependencyInjections();
        }

        private void BindSingletons()
        {
            Container
                .Bind<VocabularyController>()
                .FromComponentInNewPrefab(vocabularyControllerPrefab)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<FogEffect>()
                .FromComponentInNewPrefab(fogEffectPrefab)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<SceneLoader>()
                .AsSingle()
                .NonLazy();

            Container
                .Bind<ExpController>()
                .FromComponentInNewPrefab(scoreControllerPrefab)
                .AsSingle()
                .NonLazy();

            Container
                .Bind<EventSoundPlayer>()
                .FromComponentInNewPrefab(eventSoundPlayerPrefab)
                .AsSingle()
                .NonLazy();
        }
        
        private void BindDependencyInjections()
        {
            Container.Bind<IStorageService>().To<LocalStorageService>().AsSingle();
            Container.Bind<IAsyncTypoGenerator>().To<LstmTypoGenerator>().AsSingle();
            Container.Bind<IAsyncSentenceGenerator>().To<GptSentenceGenerator>().AsSingle();
        }
    }
}