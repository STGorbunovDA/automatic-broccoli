using AutomaticBroccoli.DataAccess.Models;

namespace AutomaticBroccoli.UnitTests.DataAccessTest
{
    public class OpenLoopTests
    {
        [Fact]
        public void Create_ReturnNewOpenLoop()
        {
            //arrage
            var openLoopId = Guid.NewGuid();
            var note = "Test note";
            var createdDate = DateTimeOffset.Now;

            //act
            var openLoop = new OpenLoop(openLoopId, note, createdDate);

            //assert
            Assert.NotNull(openLoop);
            Assert.False(string.IsNullOrWhiteSpace(openLoop.Note));
        }

        [Theory]
        [InlineData("")]
        [InlineData("   ")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Create_InvalidNoteReturnError(string invalidNote)
        {
            //arrage
            var openLoopId = Guid.NewGuid();
            var createdDate = DateTimeOffset.Now;
            Assert.Throws<ArgumentException>(() => new OpenLoop(openLoopId, invalidNote, createdDate));
        }
        [Fact]
        public void Create_InvalidCreatedDateNewOpenLoop()
        {
            //arrage
            var openLoopId = Guid.NewGuid();
            var note = "Test note";
            var createdDate = default(DateTimeOffset);

            //act - assert
            Assert.Throws<ArgumentException>(() => new OpenLoop(openLoopId, note, createdDate));
        }
    }
}