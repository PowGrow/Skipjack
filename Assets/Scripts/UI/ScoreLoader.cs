using UnityEngine;
using Zenject;

public class ScoreLoader : MonoBehaviour
{
    [SerializeField]
    private GameObject Record;
    [SerializeField]
    private GameObject Content;
    private Data _data;
    private Menu _menu;
    private DiContainer _container;
    [Inject]
    public void Construct(DiContainer container, Data data)
    {
        _container = container;
        _data = data;
    }

    private void OnEnable()
    {
        if(_menu == null)
            _menu = GetComponentInParent<Menu>();
        LoadScoreInToTable(_container, Record, _data, Content.transform);
    }

    private void LoadScoreInToTable(DiContainer container, GameObject record, Data data, Transform contentParent)
    {
        ClearOldData(contentParent);
        for(int i = 0; i < data.ScoreTable.Records.Count;  i++)
        {
            var dataValue = data.ScoreTable.Records[i];
            var recordComponent = container.InstantiatePrefabForComponent<Record>(record, contentParent);
            recordComponent.SetRecordInfo(dataValue.Position, dataValue.Name, dataValue.Score);
        }
    }

    private void ClearOldData(Transform contentParent)
    {
        foreach(Transform child in contentParent)
            Destroy(child.gameObject);
    }

    public void OnBackButtonClick()
    {
        _menu.OnBackButtonClickedd();
    }
}
