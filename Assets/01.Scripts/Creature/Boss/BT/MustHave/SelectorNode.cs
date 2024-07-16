using System.Collections.Generic;

//OR 와 같습니다. 하나라도 true면 다 true==> 하나라도 성공이면 다 성공.다 해당됨.
public class SelectorNode : Node
{
    public SelectorNode() : base()
    {
    }

    public SelectorNode(List<Node> children) : base(children)
    {
    }

    public override AllEnum.NodeState Evaluate() //내 자식들의 조건들중 하나라도 부합하거나 하나라도 이미 진행중이었다면 무조건 실행...
    {
        foreach (Node node in childrenNode)
        {
            switch (node.Evaluate())
            {
                case AllEnum.NodeState.Running:
                    return state = AllEnum.NodeState.Running;

                case AllEnum.NodeState.Success:
                    return state = AllEnum.NodeState.Success;

                case AllEnum.NodeState.Failure:
                    continue;
                default:
                    break;
            }
        }

        //나의 상태 세팅값을 넣으면서 그걸 리턴함
        return state = AllEnum.NodeState.Failure;
    }
}
