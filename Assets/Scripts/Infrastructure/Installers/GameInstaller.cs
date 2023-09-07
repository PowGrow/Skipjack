
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject PlayerInitialPosition;
    [SerializeField]
    private GameObject StrufeInitialPosition;
    [SerializeField]
    private GameObject EnemyCreatorPrefab;
    [SerializeField]
    private GameObject GameUi;
    [SerializeField]
    private GameObject MenuUi;
    [SerializeField]
    private GameObject LoadingCurtainPrefab;
    [SerializeField]
    private GameObject RestartButtonPrefab;
    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    private GameObject EnemyPrefab;
    [SerializeField]
    private int EnemyPullSize;
    [SerializeField]
    private GameObject StrufeControl;
    [SerializeField]
    private GameObject Strufe;
    [SerializeField]
    private GameObject StrufeCollision;
    [SerializeField]
    private int StepsCount;
    [SerializeField]
    private float StepDelta;

    public override void InstallBindings()
    {
        var data = new Data();
        Container.Bind<Data>().FromInstance(data).AsSingle();

        var inputService = SelectInputService();
        inputService.Activate();
        Container.Bind<IInputService>().FromInstance(inputService).AsSingle();

        var playerState = new PlayerState();
        Container.Bind<PlayerState>().FromInstance(playerState).AsSingle();

        var strufeContainer = Container.InstantiatePrefab(StrufeControl);
        IStrufeFactory strufeFactory = new StrufeFactory(strufeContainer, Strufe, Container, StepsCount, StrufeInitialPosition.transform.position);
        Container.Bind<IStrufeFactory>().FromInstance(strufeFactory).AsSingle();

        var player = Container.InstantiatePrefabForComponent<Player>(PlayerPrefab, PlayerInitialPosition.transform.position, Quaternion.identity, null);
        Container.Bind<Player>().FromInstance(player).AsSingle();

        var strufeControl =  Container.InstantiateComponent<StrufeControl>(strufeContainer);
        Container.Bind<StrufeControl>().FromInstance(strufeControl).AsSingle();

        IEnemyFactory enemyFactory = new EnemyFactory(Container, EnemyPrefab, EnemyPullSize, strufeFactory, StrufeCollision.transform.position);
        Container.Bind<IEnemyFactory>().FromInstance(enemyFactory).AsSingle();

        var strufeCollision = Container.InstantiatePrefabForComponent<StrufeCollision>(StrufeCollision);
        Container.Bind<StrufeCollision>().FromInstance(strufeCollision).AsSingle();


        var camera = Camera.main.GetComponent<CameraFollow>();
        camera.Follow(player.gameObject);

        Container.InstantiatePrefabForComponent<EnemyCreator>(EnemyCreatorPrefab);
        var score = Container.InstantiatePrefabForComponent<Score>(GameUi);
        Container.Bind<Score>().FromInstance(score).AsSingle();

        var loadingCurtain = Container.InstantiatePrefabForComponent<LoadingCurtain>(LoadingCurtainPrefab, score.transform);
        Container.Bind<LoadingCurtain>().FromInstance(loadingCurtain).AsSingle();

        var restart = Container.InstantiatePrefabForComponent<Restart>(RestartButtonPrefab, score.transform);
        Container.Bind<Restart>().FromInstance(restart).AsSingle();

        var menuUi = Container.InstantiatePrefabForComponent<Menu>(MenuUi, score.transform);
        Container.Bind<Menu>().FromInstance(menuUi).AsSingle();
    }

    private IInputService SelectInputService()
    {
        switch (Application.platform)
        {
            case RuntimePlatform.WindowsEditor:
                return new StandaloneInputService();
            case RuntimePlatform.Android:
                return new TouchScreenInputService();
            default:
                return new StandaloneInputService();
        }
    }
}
