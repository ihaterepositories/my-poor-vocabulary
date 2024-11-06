using Modules.CorrectionGameModule.TypoGeneration.Generators;
using Modules.CorrectionGameModule.TypoGeneration.Interfaces;
using Modules.VocabularyModule;
using Modules.VocabularyModule.Data.Storage.Interfaces;
using Modules.VocabularyModule.Data.Storage.Services;
using UnityEngine;
using UserInterface.Functional.ScenesLoading;
using UserInterface.Functional.ScenesLoading.Effects;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : MonoInstaller
    {
        [SerializeField] private GameObject vocabularyControllerPrefab;
        [SerializeField] private GameObject fogEffectPrefab;
        
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
        }
        
        private void BindDependencyInjections()
        {
            Container.Bind<IStorageService>().To<LocalStorageService>().AsSingle();
            Container.Bind<IAsyncTypoGenerator>().To<LstmModelClient>().AsSingle();
        }
    }
}