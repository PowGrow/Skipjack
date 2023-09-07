using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using Zenject;

public class Player : MonoBehaviour
{
    [SerializeField]
    private int ColumnsCount;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private GameObject Model;
    [SerializeField]
    private Animator Animator;
    [SerializeField]
    private AudioSource JumpAudioSource;
    [SerializeField]
    private AudioSource MoveAudioSource;

    public event Action<float> OnMoving;

    private IInputService _inputService;
    private IStrufeFactory _strufeFactory;
    private PlayerState _playerState;
    private Vector3 _initialPosition;
    private float _borderPositionValue;
    private float _moveDelta;
    private float _jumpAnimationLength;
    private float _jumpAnimationHalfDuration;
    private float _jumpAnimationEndDuration;

    private const string JumpClipName = "Jump";
    private const string JumpAnimationState = "isJumping";

    [Inject]
    public void Construct(IInputService inputSerivce, IStrufeFactory strufeFactory, PlayerState playerState)
    {
        _initialPosition = gameObject.transform.position;
        _inputService = inputSerivce;
        _strufeFactory = strufeFactory;
        _playerState = playerState;
        playerState.OnGameStart += ResetPosition;
    }

    private void Awake()
    {
        SubscribeOnEvents();
        CorrectColumnsCount();
        InitializeMoveValues();
        InitializeAnimatorValues();
    }
    private void Update()
    {
        TryToMove(gameObject.transform, Model.transform, _inputService.HorizontalAxis, _moveDelta, _borderPositionValue, Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent(out Enemy enemy))
        {
            _playerState.Set(State.GameOver);
        }
    }

    private void SubscribeOnEvents()
    {
        _inputService.OnJumpPressed += TryToJump;
    }
    private void CorrectColumnsCount()
    {
        if (ColumnsCount % 2 == 0)
            ColumnsCount--;
    }
    private void InitializeMoveValues()
    {
        _moveDelta = _strufeFactory.StrufeWidth / ColumnsCount;
        _borderPositionValue = (ColumnsCount / 2) * _moveDelta;
    }
    private void InitializeAnimatorValues()
    {
        Animator.speed = Speed;
        _jumpAnimationLength = Animator.runtimeAnimatorController.animationClips.Where(clip => clip.name == JumpClipName).First().length;
        _jumpAnimationHalfDuration = (_jumpAnimationLength / Speed) / 2;
        _jumpAnimationEndDuration = (_jumpAnimationLength / Speed) / 5;
    }


    private void TryToJump()
    {
        if (!_playerState.Is(State.Idle))
            return;
        BeginJump(Speed, Animator);
    }

    private void BeginJump(float speed, Animator animator)
    {
        _playerState.Set(State.Jumping);
        StartCoroutine(JumpDelayCoroutine(speed));
        animator.SetBool(JumpAnimationState, true);
        JumpAudioSource.Play();
    }
    private IEnumerator JumpDelayCoroutine(float speed)
    {
        yield return new WaitForSeconds(_jumpAnimationHalfDuration);
        OnMoving?.Invoke(speed);
        yield return new WaitForSeconds(_jumpAnimationEndDuration);
        EndJump();
    }
    private void EndJump()
    {
        _playerState.CheckGameOver();
        Animator.SetBool(JumpAnimationState, false);
    }

    private void TryToMove(Transform playerTransform, Transform modelTransform, float horizontalAxis, float moveDelta, float borderPositionValue, float speed)
    {
        if (horizontalAxis == 0 || !_playerState.Is(State.Idle))
            return;
        var currentPlayerPos = playerTransform.position;
        var tartgetPos = new Vector3(currentPlayerPos.x + (Mathf.Sign(horizontalAxis) * moveDelta),currentPlayerPos.y,currentPlayerPos.z);
        if (Mathf.Abs(tartgetPos.x) > borderPositionValue)
            return;
        MoveAudioSource.Play();
        StartCoroutine(MoveCoroutine(playerTransform, modelTransform, tartgetPos, Mathf.Sign(horizontalAxis), speed));
    }

    private IEnumerator MoveCoroutine(Transform playerTransform, Transform modelTransform, Vector3 targetPos, float direction, float speed)
    {
        _playerState.Set(State.Moving);

        while(playerTransform.position.x != targetPos.x)
        {
            modelTransform.rotation = Quaternion.Euler(0, direction * 90, 0);
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, targetPos, Time.deltaTime * speed);
            yield return null;
        }
        _playerState.CheckGameOver();
        modelTransform.rotation = Quaternion.identity;
    }


    private void ResetPosition()
    {
        gameObject.transform.position = _initialPosition;
    }
}