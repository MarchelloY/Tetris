using Models.api;
using strange.extensions.command.impl;

namespace Commands
{
    public class ScoreChangedCommand : Command
    {
        [Inject] public int Lines { get; set; }
    
        [Inject] public IScoreModel ScoreModel { get; set; }
    
        public override void Execute()
        {
            ScoreModel.Update(Lines);
        }
    }
}
