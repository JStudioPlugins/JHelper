using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JHelper.Weapons
{
    public class Weapon : IEquatable<Weapon>
    {
        public ushort WeaponId { get; set; }
        public ushort AmmoId { get; set; }
        public ushort AmmoAmount { get; set; }

        public ushort SightId { get; set; }
        public ushort TacticalId { get; set; }
        public ushort GripId { get; set; }
        public ushort BarrelId { get; set; }

        public EFiremode FireMode { get; set; }
        public bool TacticalSetting { get; set; }
        public byte BarrelQuality { get; set; }

        public Weapon(ushort weaponId, ushort ammoId, EFiremode fireMode, bool tacticalSetting, ushort ammoAmount, ushort sightId, ushort tacticalId, ushort gripId, ushort barrelId, byte barrelQuality)
        {
            WeaponId = weaponId;
            AmmoId = ammoId;
            AmmoAmount = ammoAmount;
            SightId = sightId;
            TacticalId = tacticalId;
            GripId = gripId;
            BarrelId = barrelId;
            FireMode = FireMode;
            TacticalSetting = tacticalSetting;
            BarrelQuality = barrelQuality;
        }

        public Weapon()
        {

        }

        public byte[] ObtainState()
        {
            byte[] state = new byte[18];

            if (SightId != 0)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(SightId), 0, state, 0, 2);

            }
            if (TacticalId != 0)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(TacticalId), 0, state, 2, 2);
            }
            if (GripId != 0)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(GripId), 0, state, 4, 2);
            }
            if (BarrelId != 0)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(BarrelId), 0, state, 6, 2);

                Buffer.BlockCopy(BitConverter.GetBytes(BarrelQuality), 0, state, 16, 1);
            }
            if (AmmoId != 0)
            {
                Buffer.BlockCopy(BitConverter.GetBytes(AmmoId), 0, state, 8, 2);

                Buffer.BlockCopy(BitConverter.GetBytes(AmmoAmount), 0, state, 10, 1);
            }
            Buffer.BlockCopy(BitConverter.GetBytes((byte)FireMode), 0, state, 11, 1);
            Buffer.BlockCopy(BitConverter.GetBytes(TacticalSetting), 0, state, 12, 1);

            Buffer.BlockCopy(BitConverter.GetBytes((byte)100), 0, state, 13, 1);
            Buffer.BlockCopy(BitConverter.GetBytes((byte)100), 0, state, 14, 1);
            Buffer.BlockCopy(BitConverter.GetBytes((byte)100), 0, state, 15, 1);
            Buffer.BlockCopy(BitConverter.GetBytes((byte)100), 0, state, 17, 1);

            return state;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Weapon);
        }

        public bool Equals(Weapon other)
        {
            return other != null &&
                   WeaponId == other.WeaponId &&
                   AmmoId == other.AmmoId &&
                   AmmoAmount == other.AmmoAmount &&
                   SightId == other.SightId &&
                   TacticalId == other.TacticalId &&
                   GripId == other.GripId &&
                   BarrelId == other.BarrelId &&
                   FireMode == other.FireMode &&
                   TacticalSetting == other.TacticalSetting &&
                   BarrelQuality == other.BarrelQuality;
        }

        public override int GetHashCode()
        {
            int hashCode = -450042863;
            hashCode = hashCode * -1521134295 + WeaponId.GetHashCode();
            hashCode = hashCode * -1521134295 + AmmoId.GetHashCode();
            hashCode = hashCode * -1521134295 + AmmoAmount.GetHashCode();
            hashCode = hashCode * -1521134295 + SightId.GetHashCode();
            hashCode = hashCode * -1521134295 + TacticalId.GetHashCode();
            hashCode = hashCode * -1521134295 + GripId.GetHashCode();
            hashCode = hashCode * -1521134295 + BarrelId.GetHashCode();
            hashCode = hashCode * -1521134295 + FireMode.GetHashCode();
            hashCode = hashCode * -1521134295 + TacticalSetting.GetHashCode();
            hashCode = hashCode * -1521134295 + BarrelQuality.GetHashCode();
            return hashCode;
        }

        public static bool operator ==(Weapon left, Weapon right)
        {
            return EqualityComparer<Weapon>.Default.Equals(left, right);
        }

        public static bool operator !=(Weapon left, Weapon right)
        {
            return !(left == right);
        }
    }
}
