using UnityEngine;

namespace Towers
{
   public interface ITowerInteract
   {
      void TakeDamage(int damage, Type damageType);
   }
}
