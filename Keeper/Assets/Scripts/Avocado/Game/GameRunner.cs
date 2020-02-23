using Pixeye.Actors;

namespace Avocado.Game
{
    public class GameRunner : Starter
    {
        //public static Buffer<SegmentAttack> BufferAttack = new Buffer<SegmentAttack>(1000);

        // Метод инициализации в стартере
        protected override void Setup()
        {
            // Добавляйте новые процессоры через метод Add<T>
            /*Add<ProcessorHelloWorld>();
            Add<ProcessorHelloWorldAlt>();
            Add<ProcessorExampleFilters>();
            Add<ProcessorBuffer>();
            Add<ProcessorParallel>();
            Add<ProcessorActorsExample>();
            Add<ProcessorEntities>();
            Add<ProcessorSignalExample>();
            Add<ProcessorTimers>();*/
        }

        // Используйте метод для чистки после работы сцены. 
        // Метод вызовется когда вы поменяете сцену.
        protected override void Dispose()
        {
            // clear buffer when the scene is removed
           // BufferAttack.Clear();
        }
    }
}