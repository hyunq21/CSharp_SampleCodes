using System;
using System.IO;
using Google.FlatBuffers;
using MyGame.Sample;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace FlatBuffers_sample2
{
    class Program
    {
        static void Main(string[] args)
        {

            var builder = new FlatBufferBuilder(1024);
            var weaponOneName = builder.CreateString("Sword");
            var weaponOneDamage = 3;

            var weaponTwoName = builder.CreateString("Axe");
            var weaponTwoDamage = 5;

            // Use the `CreateWeapon()` helper function to create the weapons, since we set every field.
            var sword = Weapon.CreateWeapon(builder, weaponOneName, (short)weaponOneDamage);
            var axe = Weapon.CreateWeapon(builder, weaponTwoName, (short)weaponTwoDamage);






            // This snippet ignores exceptions for brevity.
            byte[] data = File.ReadAllBytes("monsterdata_test.mon");

            ByteBuffer bb = new ByteBuffer(data);
            Monster monster = Monster.GetRootAsMonster(bb);

            // property
            short hp = monster.Hp;
            Vec3? pos = monster.Pos;

            // method filling a preconstructed object
            var preconstructedPos = new Vec3();
            //monster.GetPos(preconstructedPos);


            // Deserialize from buffer into object.
            MonsterT monsterobj = GetMonster(flatbuffer).UnPack();

            // Update object directly like a C# class instance.
            Console.WriteLine(monsterobj.Name);
            monsterobj.Name = "Bob";  // Change the name.

            // Serialize into new flatbuffer.
            FlatBufferBuilder fbb = new FlatBufferBuilder(1);
            fbb.Finish(Monster.Pack(fbb, monsterobj).Value);


            // Deserialize MonsterT from json
            string jsonText = File.ReadAllText(@"Resources/monsterdata_test.json");
            MonsterT mon = MonsterT.DeserializeFromJson(jsonText);

            // Serialize MonsterT to json
            string jsonText2 = mon.SerializeToJson();




            Console.WriteLine("Hello World!");
        }
    }
}
