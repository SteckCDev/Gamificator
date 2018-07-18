namespace Gamification
{
	class XP
	{
		private readonly float[] xpCoefficent; // XP increase coefficent
		private int targetXp;                  // XP need to up level
		private int currentXp;                 // XP on current time
		private int currentLvl;                // LEVEL on current time

		public XP(float[] xpCoefficent, int targetXp, int currentXp, int currentLvl)
		{
			this.targetXp = targetXp;
			this.xpCoefficent = xpCoefficent;
			this.currentXp = currentXp;
			this.currentLvl = currentLvl;
		}

		public int GetXP()     { return currentXp; }

		public int GetTarget() { return targetXp; }

		public int GetLevel()  { return currentLvl; }

		public void IncreaseXp(int inc)
		{
			currentXp += inc;

			if (currentXp >= targetXp)
			{
				LevelUp();
			}
		}

		public void LevelUp()
		{
			currentXp -= targetXp;
			targetXp = (int)(targetXp * xpCoefficent[currentLvl % 2]);
			currentLvl++;
		}
	}
}
