//using UnityEngine;
//using UnityEngine.Events;

//[System.Serializable]
//public class CustomGameEvent : UnityEvent<Component, object> { };
//public class MultiGameEventListener
//{
//    [SerializeField] private GameEventSO gameEvent;
//    [SerializeField] private CustomGameEvent response;


//    //private void OnEnable()
//    //{
//    //    //gameEvent.RegisterListener(this);
//    //}

//    //private void OnDisable()
//    //{
//    //    //gameEvent.UnRegisterListener(this);
//    //}

//    public void OnEventRaised(Component sender, object data)
//    {
//        response.Invoke(sender, data);
//    }
//}
