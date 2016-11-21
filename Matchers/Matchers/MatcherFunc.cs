namespace NetSpec.Matchers
{
    public struct MatcherFunc<T> : Matcher
    {
        public let matcher: (Expression<T>, FailureMessage) throws -> Bool

        public init(_ matcher: @escaping (Expression<T>, FailureMessage) throws -> Bool)
        {
            self.matcher = matcher
        }

        public func matches(_ actualExpression: Expression<T>, failureMessage: FailureMessage) throws -> Bool {
        return try matcher(actualExpression, failureMessage)
        }

    public func doesNotMatch(_ actualExpression: Expression<T>, failureMessage: FailureMessage) throws -> Bool {
        return try !matcher(actualExpression, failureMessage)
    }
}


public struct NonNilMatcherFunc<T> : Matcher
{
    public let matcher: (Expression<T>, FailureMessage) throws -> Bool

    public init(_ matcher: @escaping (Expression<T>, FailureMessage) throws -> Bool)
    {
        self.matcher = matcher
    }

    public func matches(_ actualExpression: Expression<T>, failureMessage: FailureMessage) throws -> Bool {
        let pass = try matcher(actualExpression, failureMessage)
        if try attachNilErrorIfNeeded(actualExpression, failureMessage: failureMessage)
    {
        return false
        }
        return pass
}

public func doesNotMatch(_ actualExpression: Expression<T>, failureMessage: FailureMessage) throws -> Bool {
        let pass = try !matcher(actualExpression, failureMessage)
        if try attachNilErrorIfNeeded(actualExpression, failureMessage: failureMessage)
{
    return false
        }
        return pass
    }

    internal func attachNilErrorIfNeeded(_ actualExpression: Expression<T>, failureMessage: FailureMessage) throws -> Bool {
        if try actualExpression.evaluate() == nil {
            failureMessage.postfixActual = " (use beNil() to match nils)"
            return true
        }
        return false
    }
}
}