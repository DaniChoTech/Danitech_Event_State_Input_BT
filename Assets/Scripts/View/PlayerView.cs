using System.ComponentModel;
using UnityEngine;
using ViewModel.Extensions;

public class PlayerView : MonoBehaviour
{
    [SerializeField] TextMesh TextMesh_Name;
    [SerializeField] GameObject Prefab_SpecialLevelUp;

    public TextMesh TextMesh_Level;
    public Animator Animator_Player;

    private PlayerViewModel _vm;
    private IState _curState;

    private void Start()
    {
        ChangeState(new IdleState(this));
    }

    public void ChangeState(IState newState)
    {
        _curState?.ExitState();
        _curState = newState;
        _curState.EnterState();
    }

    private void OnEnable()
    {
        if (_vm == null)
        {
            _vm = new PlayerViewModel();
            _vm.PropertyChanged += OnPropertyChanged;
            _vm.RegisterEventsOnEnable();
            _vm.RefreshViewModel();
        }
    }

    private void Update()
    {
        _curState?.ExecuteOnUpdate();
    }

    private void OnDisable()
    {
        if (_vm != null)
        {
            _vm.UnRegisterOnDisable();
            _vm.PropertyChanged -= OnPropertyChanged;
            _vm = null;
        }
    }

    private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(_vm.Name):
                TextMesh_Name.text = $"{_vm.Name}";
                break;
            case nameof(_vm.Level):
                TextMesh_Level.text = $"Lv.{_vm.Level}";
                Animator_Player.SetTrigger("LevelUp");
                CheckSpecialLevelUP(_vm.Level);
                break;
        }
    }

    private void CheckSpecialLevelUP(int level)
    {
        if (level % 10 == 0)
        {
            Instantiate(Prefab_SpecialLevelUp, this.transform);
        }
    }


}
