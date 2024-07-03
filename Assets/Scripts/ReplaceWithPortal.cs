using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplaceWithPortal : MonoBehaviour
{
    public GameObject portalPrefab;

    private void Start()
    {
        // Suscribirse al evento onPortalEnabled del GameController
        GameController.Instance.onPortalEnabled.AddListener(ReplaceWithPortalObject);
    }

    private void OnDisable()
    {
        // Desuscribirse del evento onPortalEnabled del GameController
        if (GameController.Instance != null)
        {
            GameController.Instance.onPortalEnabled.RemoveListener(ReplaceWithPortalObject);
        }
    }

    private void ReplaceWithPortalObject()
    {
        // Instancia el prefab del portal en la posición y rotación de la pared
        Instantiate(portalPrefab, transform.position, transform.rotation);

        // Destruye el objeto de la pared
        Destroy(gameObject);
    }
}
