using System;

namespace Gamification
{
	class XP
	{
		private float[] xpCoefficent;       // XP increase coefficent
		private int targetXp;               // XP need to up level
		private int currentXp;              // XP on current time
		private int currentLvl;             // Current LVL

		public XP(int targetXp, float[] xpCoefficent, int currentXp, int currentLvl)
		{
			this.targetXp = targetXp;
			this.xpCoefficent = xpCoefficent;
			this.currentXp = currentXp;
			this.currentLvl = currentLvl;
		}

		public int GetXP() { return currentXp; }

		public int GetTarget() { return targetXp; }

		public int GetLevel()  { return currentLvl; }

		public void IncreaseXp(int inc)
		{
			currentXp += inc;
			CheckXp();
		}

		private void CheckXp()
		{
			if (currentXp >= targetXp)
			{
				LevelUp();
			}
		}

		public void LevelUp()
		{
			currentXp -= targetXp;

			float temp = targetXp;
			temp *= xpCoefficent[currentLvl % 2];
			targetXp = (int)temp;

			currentLvl++;
		}
	}
}
