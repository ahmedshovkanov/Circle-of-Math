using UnityEngine;

public class FollowTouch : MonoBehaviour
{
    private Camera _mainCamera;
    private bool isFollowing = false;

    private void Start()
    {
        _mainCamera = GameFlowController.instance.GlobalCameraRef;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("TouchPhase.Began");
                Ray ray = _mainCamera.ScreenPointToRay(touch.position);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit) && hit.transform == transform)
                {
                    Debug.Log(hit.transform.name);
                    isFollowing = true;
                }
            }
            else if (touch.phase == TouchPhase.Moved && isFollowing)
            {
                Vector3 touchWorldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, _mainCamera.nearClipPlane));
                transform.position = new Vector3(touchWorldPosition.x, touchWorldPosition.y, transform.position.z);
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                isFollowing = false;
            }
        }
    }
}