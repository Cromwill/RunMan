using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IDeadable
{
    [SerializeField] private PlayerRepository _playerRepository;
    [SerializeField] private Booster[] _boosters;
    [SerializeField] private SkillData[] _skills;

    private PlayerSkins _skin;
    private PlayerMover _mover;
    private Booster[] _currentBooster;

    public event Action Deading;

    public ScoreCounter scoreCounter { get; private set; }
    public bool isDead { get; private set; }

    private void Awake()
    {
        scoreCounter = GetComponent<ScoreCounter>();
        _mover = GetComponent<PlayerMover>();
        scoreCounter.Initialization();
        _skin = GetComponent<PlayerSkins>();
    }

    private void Start()
    {
        var currentSkin = _playerRepository.GetCurrentSkin();
        if (currentSkin != null)
            _skin.SetSkin(currentSkin);

        _currentBooster = FillingCurrentBooster().ToArray();
        var playerComponents = GetComponents<IPlayerComponent>();

        foreach (Booster booster in _currentBooster)
        {
            foreach (var component in playerComponents)
            {
                if (component.BoosterType == booster.Type)
                    component.Initialization(booster);
            }
        }

        foreach (var skill in _skills)
            skill.UseSkill(playerComponents);
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            _mover.Move();
            scoreCounter.DistanceColculate();
        }
    }

    public void Turn(RotateDirection direction) => _mover.Turn(direction);

    public void Dead()
    {
        if (!isDead)
        {
            Deading?.Invoke();
            isDead = true;
        }
    }

    private IEnumerable<Booster> FillingCurrentBooster()
    {
        List<Booster> currentBooster = new List<Booster>();

        foreach (var booster in _boosters)
        {
            if (SaveDataStorage.ItemContain(booster))
            {
                currentBooster.Add(booster);
            }
        }
        return currentBooster;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<MapElement>() != null)
            _mover.ResetSpeed();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.GetComponent<MapElement>() != null)
            _mover.UseMaxRotationSpeed();
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<MapElement>() != null)
            _mover.ResetRotationSpeed();
    }
}

public enum RotateDirection
{
    Left = -1,
    Right = 1
}