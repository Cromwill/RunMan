using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class ArmorInfo : MonoBehaviour
{
    [SerializeField] private Text _bulletCountViewer;

    private Animator _selfAnimator;
    private int _currentBulletCount;

    private void Start()
    {
        _selfAnimator = GetComponent<Animator>();
    }

    public void Show(int count)
    {
        _selfAnimator.Play("AmmunationFire");
        _currentBulletCount = count;
    }

    public void ChangeValue()
    {
        _bulletCountViewer.text = _currentBulletCount.ToString();
    }
}
