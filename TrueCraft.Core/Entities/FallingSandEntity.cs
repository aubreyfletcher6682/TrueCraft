﻿using System;
using TrueCraft.API;
using TrueCraft.API.Networking;
using TrueCraft.Core.Networking.Packets;
using TrueCraft.API.Entities;
using TrueCraft.API.Server;
using TrueCraft.API.World;
using TrueCraft.Core.Logic.Blocks;

namespace TrueCraft.Core.Entities
{
    public class FallingSandEntity : ObjectEntity, IAABBEntity
    {
        public FallingSandEntity(Vector3 position)
        {
            _Position = position + new Vector3(0.5);
        }

        public override byte EntityType { get { return 70; } }

        public override Size Size
        {
            get
            {
                return new Size(1);
            }
        }

        public override IPacket SpawnPacket
        {
            get
            {
                return new SpawnGenericEntityPacket(EntityID, (sbyte)EntityType,
                    MathHelper.CreateAbsoluteInt(Position.X), MathHelper.CreateAbsoluteInt(Position.Y),
                    MathHelper.CreateAbsoluteInt(Position.Z), 0, null, null, null);
            }
        }

        public override int Data { get { return 1; } }

        public void TerrainCollision(Vector3 collisionPoint, Vector3 collisionDirection)
        {
            if (collisionDirection == Vector3.Down)
            {
                EntityManager.DespawnEntity(this);
                World.SetBlockID((Coordinates3D)_Position, SandBlock.BlockID);
            }
        }

        public BoundingBox BoundingBox
        {
            get
            {
                return new BoundingBox(Vector3.Zero, Vector3.One);
            }
        }

        public bool BeginUpdate()
        {
            EnablePropertyChange = false;
            return true;
        }

        public void EndUpdate(Vector3 newPosition)
        {
            EnablePropertyChange = true;
            Position = newPosition;
        }

        public float AccelerationDueToGravity
        {
            get
            {
                return 0.4f;
            }
        }

        public float Drag
        {
            get
            {
                return 0.2f;
            }
        }

        public float TerminalVelocity
        {
            get
            {
                return 1.96f;
            }
        }
    }
}