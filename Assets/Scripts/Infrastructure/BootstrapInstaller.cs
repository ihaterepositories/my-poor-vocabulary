using Modules.MiniGamesCore.CorrectionGameModule.Data.Generation.TypoGenerators;
using Modules.MiniGamesCore.CorrectionGameModule.Data.Generation.TypoGenerators.Interfaces;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators;
using Modules.MiniGamesCore.PasteGameModule.Data.Generation.SentenceGenerators.Interfaces;
using Modules.ScoreModule;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Storage.Interfaces;
using Modules.VocabularyModule.Data.Storage.Services;
using UnityEngine;
using UserInterface.Functional.Navigation;
using UserInterface.Functional.Navigation.Effects;
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
                .Bind<ScoreController>()
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
            Container.Bind<IAsyncSentenceGenerator>().To<MockSentenceGenerator>().AsSingle();
        }
    }
}