using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gamification
{
	class XP
	{
		private float[] xpCoefficent;       // XP increase coefficent
		private int xpTarget  { get; set; } // XP need to up level
		private int currentXp { get; set; } // XP on current time
		private int currentLvl;             // Current LVL

		public XP(int xpTarget, float[] xpCoefficent)
		{
			this.xpTarget = xpTarget;
			this.xpCoefficent = xpCoefficent;
		}

		private void LevelUp()
		{
			float temp = xpTarget;
			temp *= xpCoefficent[currentLvl % 2];
			xpTarget = (int)temp;
			currentLvl++;
		}
	}
}
