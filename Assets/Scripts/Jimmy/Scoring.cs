public class Scoring
{
    private float _mScore;

    public enum Param
    {
        Add,
        Subtract,
        Multiply,
        Divide
    }
    
    public float ChangeScore(Param parameter, float currentScore, float variable)
    {
        _mScore = 0;
        switch (parameter)
        {
            case Param.Add:
                _mScore += currentScore + variable;
                return _mScore;
            case Param.Subtract:
                _mScore += currentScore - variable;
                return _mScore;
            case Param.Multiply:
                _mScore = currentScore;
                _mScore += currentScore * variable;
                return _mScore;
            case Param.Divide:
                _mScore = currentScore;
                _mScore += currentScore / variable;
                return _mScore;
        }
        return 0;
    }
}