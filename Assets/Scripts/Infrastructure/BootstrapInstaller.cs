using Modules.CorrectionGameModule.TypoGeneration.Generators;
using Modules.CorrectionGameModule.TypoGeneration.Interfaces;
using Modules.General.Navigation;
using Modules.General.Navigation.Effects;
using Modules.General.Score;
using Modules.General.Sound;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Storage.Interfaces;
using Modules.VocabularyModule.Data.Storage.Services;
using UnityEngine;
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
            Container.Bind<IAsyncTypoGenerator>().To<LstmModelClient>().AsSingle();
        }
    }
}