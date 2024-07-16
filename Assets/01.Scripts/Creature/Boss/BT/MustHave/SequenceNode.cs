using System.Collections.Generic;

//AND 조건. 하나라도 false면 다 false
public class SequenceNode : Node
{
    public SequenceNode() : base()
    {
    }

    public SequenceNode(List<Node> children) : base(children)
    {
    }

    public override AllEnum.NodeState Evaluate() 
    {
        bool isRunning = false;
        //자식들을 돌면서 조건체크함.
        //하나라도 불합격이면
        //더이상 다른자식들을 체크해보지않고 나감.
        foreach (Node node in childrenNode)
        {
            switch (node.Evaluate())
            {
                case AllEnum.NodeState.Running:
                    isRunning = true;
                    continue;

                case AllEnum.NodeState.Success:
                    continue;

                case AllEnum.NodeState.Failure:
                    return AllEnum.NodeState.Failure;
                default:
                    break;
            }
        }

        //나의 상태 세팅값을 넣으면서 그걸 리턴함
        return state = isRunning ? AllEnum.NodeState.Running : AllEnum.NodeState.Success;
    }
}
