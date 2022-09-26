//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;

//namespace Asklepios.Data.DBContexts
//{
//    public static class ModelBuilderExtension
//    {
//        public static void HasJoinData<TFirst, TSecond>(
//            this ModelBuilder modelBuilder,
//            params (TFirst First, TSecond Second)[] data)
//            where TFirst : class where TSecond : class
//            => modelBuilder.HasJoinData(data.AsEnumerable());

//        public static void HasJoinData<TFirst, TSecond>(
//            this ModelBuilder modelBuilder,
//            IEnumerable<(TFirst First, TSecond Second)> data)
//            where TFirst : class where TSecond : class
//        {
//            var firstEntityType = modelBuilder.Model.FindEntityType(typeof(TFirst));
//            var secondEntityType = modelBuilder.Model.FindEntityType(typeof(TSecond));
//            var firstToSecond = firstEntityType.GetSkipNavigations()
//                .Single(n => n.TargetEntityType == secondEntityType);
//            var joinEntityType = firstToSecond.JoinEntityType;
//            var firstProperty = firstToSecond.ForeignKey.Properties.Single();
//            var secondProperty = firstToSecond.Inverse.ForeignKey.Properties.Single();
//            var firstValueGetter = firstToSecond.ForeignKey.PrincipalKey.Properties.Single().GetGetter();
//            var secondValueGetter = firstToSecond.Inverse.ForeignKey.PrincipalKey.Properties.Single().GetGetter();
//            var seedData = data.Select(e => (object)new Dictionary<string, object>
//            {
//                [firstProperty.Name] = firstValueGetter.GetClrValue(e.First),
//                [secondProperty.Name] = secondValueGetter.GetClrValue(e.Second),
//            });
//            modelBuilder.Entity(joinEntityType.Name).HasData(seedData);
//        }

//    }
//}
