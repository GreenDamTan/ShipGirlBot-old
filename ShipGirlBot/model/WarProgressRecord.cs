using System;
using System.Collections.Generic;

public class WarProgressRecord
{
    private GetBattleResultResponse _warResponse;
    private WarResult _warResult;
    public AirControlType airControlType;
    public int bossHp;
    public int bossHpLeft;
    public int canDoNightWar;
    public AttackParam[] closeTorpedoAttack;
    public int currentNormalAttackIndex;
    public BuffParam[] enemyBuffs;
    public UserFleet enemyFleet;
    private ShipInWarInitStatus[] enemyInitShipStatus;
    public string enemyName;
    public BuffParam[] enemyNightBuffs;
    public ShipInWar[] enemyShips;
    public int hasExploreBuff;
    public int[] hpBeforeNightWarEnemy;
    public int[] hpBeforeNightWarSelf;
    public int isExploreSuccess;
    public int[] lockedTargetEnemy;
    public int[] lockedTargetSelf;
    public AttackParam[] nightAttacks;
    public AttackParam[] normalAttacks;
    public AttackParam[] openAirAttack;
    public int[] openAirAttackDefEnemy;
    public int[] openAirAttackDefSelf;
    public AttackParam[] openAntiSubAttack;
    public AttackParam[] openTorpedoAttack;
    public BuffParam[] selfBuffs;
    public UserFleet selfFleet;
    private ShipInWarInitStatus[] selfInitShipStatus;
    public BuffParam[] selfNightBuffs;
    public ShipInWar[] selfShips;
    public string userName;
    public WarTypes warType;

    public void DoInitCheck()
    {
        this.selfInitShipStatus = new ShipInWarInitStatus[this.selfShips.Length];
        for (int i = 0; i < this.selfShips.Length; i++)
        {
            ShipInWar war = this.selfShips[i];
            war.InitBrokenType();
            this.selfInitShipStatus[i] = new ShipInWarInitStatus { hp = war.hp, hpMax = war.hpMax };
        }
        this.enemyInitShipStatus = new ShipInWarInitStatus[this.enemyShips.Length];
        for (int j = 0; j < this.enemyShips.Length; j++)
        {
            ShipInWar war2 = this.enemyShips[j];
            war2.InitBrokenType();
            this.enemyInitShipStatus[j] = new ShipInWarInitStatus { hp = war2.hp, hpMax = war2.hpMax };
        }
    }

    public ShipInWar GetAttackerShip(AttackParam ap)
    {
        if (ap.attackSide == AttackSide.Self)
        {
            return this.selfShips[ap.fromIndex];
        }
        return this.enemyShips[ap.fromIndex];
    }

    public AttackParam GetNextAttack()
    {
        if (this.currentNormalAttackIndex < this.normalAttacks.Length)
        {
            AttackParam param = this.normalAttacks[this.currentNormalAttackIndex];
            this.currentNormalAttackIndex++;
            return param;
        }
        return null;
    }

    public List<RecoveryParam> GetRecoveryShipOfAttacks(AttackParam[] attacks)
    {
        List<RecoveryParam> list = new List<RecoveryParam>();
        foreach (AttackParam param in attacks)
        {
            if ((param.recovery != null) && (param.recovery.Length > 0))
            {
                list.AddRange(param.recovery);
            }
        }
        return list;
    }

    public void SetShipStatusToScene3Close()
    {
        if ((this.hpBeforeNightWarSelf != null) && (this.hpBeforeNightWarSelf.Length > 0))
        {
            for (int i = 0; i < this.selfShips.Length; i++)
            {
                ShipInWar war = this.selfShips[i];
                war.hp = this.hpBeforeNightWarSelf[i];
            }
        }
        if ((this.hpBeforeNightWarEnemy != null) && (this.hpBeforeNightWarEnemy.Length > 0))
        {
            for (int j = 0; j < this.enemyShips.Length; j++)
            {
                ShipInWar war2 = this.enemyShips[j];
                war2.hp = this.hpBeforeNightWarEnemy[j];
            }
        }
    }

    public void SetShipStautsToScene5End()
    {
    }

    public bool CanEnemyDoAirAttack
    {
        get
        {
            foreach (AttackParam param in this.openAirAttack)
            {
                if (param.attackSide == AttackSide.Enemy)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public bool CanSelfDoAirAttack
    {
        get
        {
            foreach (AttackParam param in this.openAirAttack)
            {
                if (param.attackSide == AttackSide.Self)
                {
                    return true;
                }
            }
            return false;
        }
    }

    public ShipInWar EnemyAirAttackShip
    {
        get
        {
            foreach (ShipInWar war in this.enemyShips)
            {
                if (war.CanDoAirAttack)
                {
                    return war;
                }
            }
            return this.enemyShips[0];
        }
    }

    public bool HaveCloseTorpedoFight
    {
        get
        {
            return ((this.closeTorpedoAttack != null) && (this.closeTorpedoAttack.Length > 0));
        }
    }

    public bool HaveNightNormalAttack
    {
        get
        {
            return ((this.nightAttacks != null) && (this.nightAttacks.Length > 0));
        }
    }

    public bool HaveOpenAntiSubmarineFight
    {
        get
        {
            return ((this.openAntiSubAttack != null) && (this.openAntiSubAttack.Length > 0));
        }
    }
    public bool HaveOpenTorpedoFight
    {
        get
        {
            return ((this.openTorpedoAttack != null) && (this.openTorpedoAttack.Length > 0));
        }
    }

    public List<RecoveryParam> RecoveryShipsInAirAttack
    {
        get
        {
            return this.GetRecoveryShipOfAttacks(this.openAirAttack);
        }
    }

    public List<RecoveryParam> RecoveryShipsInOpenAntiSubAttack
    {
        get
        {
            return this.GetRecoveryShipOfAttacks(this.openAntiSubAttack);
        }
    }

    public List<RecoveryParam> RecoveryShipsInCloseTorpedoAttack
    {
        get
        {
            return this.GetRecoveryShipOfAttacks(this.closeTorpedoAttack);
        }
    }

    public List<RecoveryParam> RecoveryShipsInOpenTorpedoAttack
    {
        get
        {
            return this.GetRecoveryShipOfAttacks(this.openTorpedoAttack);
        }
    }

    public ShipInWar SelfAirAttackShip
    {
        get
        {
            foreach (ShipInWar war in this.selfShips)
            {
                if (war.CanDoAirAttack)
                {
                    return war;
                }
            }
            return this.selfShips[0];
        }
    }

    public GetBattleResultResponse WarResponse
    {
        get
        {
            return this._warResponse;
        }
        set
        {
            this._warResponse = value;
        }
    }

    public WarResult WarResult
    {
        get
        {
            return this._warResult;
        }
        set
        {
            this._warResult = value;
        }
    }
}

