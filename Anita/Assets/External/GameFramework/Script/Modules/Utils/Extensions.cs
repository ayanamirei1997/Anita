using UnityEngine;
using System.Linq;

namespace Anita
{
    public static class RendererExtensions
    {
        public static bool isVisible(this Renderer renderer, Camera camera)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }
    }

    public static class GameObjectExtensions
    {
        public static T GetOrAddCompoment<T>(this GameObject gameobject) where T : Component
        {
            T result = gameobject.GetComponent<T>();
            if (result == null)
            {
                result = gameobject.AddComponent<T>();
            }
            return result;
        }

        public static void SetLayerRecursively(this GameObject gameObject, int layer)
        {
            gameObject.layer = layer;
            for (int i = 0, imax = gameObject.transform.childCount; i < imax; i++)
            {
                SetLayerRecursively(gameObject.transform.GetChild(i).gameObject, layer);
            }
        }

        public static T[] GetComponentsInChildrenWithoutSelf<T>(this GameObject self) where T : Component
        {
            return self.GetComponentsInChildren<T>().Where(c => self != c.gameObject).ToArray();
        }
    }

    public static class TransformExtensions
    {
        public static T[] GetComponentsInChildrenWithoutSelf<T>(this Transform self) where T : Component
        {
            return self.GetComponentsInChildren<T>().Where(c => self != c.transform).ToArray();
        }
    }

    public static class ComponentExtensions
    {
        public static T AddComponent<T>(this Component component) where T : Component
        {
            return component.gameObject.AddComponent<T>();
        }

        public static T GetOrAddCompoment<T>(this Component component) where T : Component
        {
            T result = component.GetComponent<T>();
            if (result == null)
            {
                result = component.AddComponent<T>();
            }
            return result;
        }
    }
}