using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if DoTween
public class DelayTest : MonoBehaviour {

    public DG.Tweening.DOTweenAnimation ani;

    public UnityEngine.UI.Button btn;

    private int click = 0;
	// Use this for initialization
	void Start () {
        DelaySqueue squeue = new DelaySqueue();
        Delay delay1 = new TimeDelay(1, () => Debug.Log(Time.time), () => Debug.Log("xxxxx"));
        Delay delay2 = new DotweenDelay(ani);
        Delay delay3 = new TimeDelay(1, null, ()=> btn.onClick.AddListener(()=>click++));
        Delay delay4 = new ConditionDelay(()=>click == 3,null);
        Delay delay5 = new TimeDelay(1, () => Debug.Log(Time.time), () => Debug.Log("yyyy"));
        squeue.Add(delay1, delay2,delay3,delay4,delay5);
        squeue.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}
}
#endif