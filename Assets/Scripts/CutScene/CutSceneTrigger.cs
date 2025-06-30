using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CutSceneTrigger : MonoBehaviour
{
    public PlayableDirector director;
    public CinemachineCamera mainCam;
    public CinemachineCamera cutSceneCam;
    public CinemachineCamera ultimateCam;

    public static CutSceneTrigger instance { get; set; }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(transform.root.gameObject);
            return;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        director.stopped += OnCutsceneEnd;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cutSceneCam.Priority = 20;
            mainCam.Priority = 10;
            director.Play();
        }
    }
    void OnCutsceneEnd(PlayableDirector pd)
    {
        mainCam.Priority = 20;
        cutSceneCam.Priority = 10;
    }

    public void StageClear()
    {
        mainCam.Priority = 10;
        ultimateCam.Priority = 20;

        Vector3 offsetDir = Quaternion.AngleAxis(-45f, Vector3.up) * PlayerController.Instance.transform.forward;
        Vector3 camPos = PlayerController.Instance.transform.position + offsetDir * 4f + Vector3.up * 1f;
        Quaternion camRot = Quaternion.LookRotation(PlayerController.Instance.transform.position + Vector3.up - camPos);

        ultimateCam.transform.SetPositionAndRotation(camPos, camRot);
    }
}
