using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorBase : IPlayerStatus 
{
	protected class ActorInfo
	{
		int CharaID;
		string Name;
		int HP;
		int Atk;
		int Def;
		int MoveSpeed;

		public ActorInfo(int CharaID, string Name, int HP, int Atk, int Def, int MoveSpeed)
		{
			this.CharaID = CharaID;
			this.Name = Name;
			this.HP = HP;
			this.Atk = Atk;
			this.Def = Def;
			this.MoveSpeed = MoveSpeed;
		}

	}


	public override void OnStart()
	{

	}
	public override void OnUpdate()
    {

    }
	public override void OnExit()
    {

    }

}
