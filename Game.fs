module Game
    open System.Numerics
    open System

    type Triangle = {
        Top: Vector2;
        Left: Vector2;
        Right: Vector2;
    }

    type GameState = {
        mutable Triangle: Triangle 
        mutable Speed: Vector2
    }

    let mkGameState() = 
        {
            Triangle = { Top = Vector2(0f, 0.25f); Left = Vector2(0.25f, -0.25f ); Right = Vector2(-0.25f, -0.25f) };
            Speed = Vector2(0.4f, 0.5f)
        }

    let private outOfBoundsX (point: Vector2) =
        point.X < -1.0f || point.X > 1.0f

    let private outOfBoundsY (point: Vector2) =
        point.Y < -1.0f || point.Y > 1.0f

    let private outOfBoundsTriangleX p1 p2 p3 =
        outOfBoundsX p1 || outOfBoundsX p2 || outOfBoundsX p3
    
    let private outOfBoundsTriangleY p1 p2 p3 =
        outOfBoundsY p1 || outOfBoundsY p2 || outOfBoundsY p3

    let update (gameState: GameState) (timeElapsed: float) =
        let triangle = gameState.Triangle
        
        let triangleCenter = (triangle.Top + triangle.Left + triangle.Right) / 3.0f
        let rotSpeedPerSecond = Math.PI / 7.0
        let rotSpeedPerMs = rotSpeedPerSecond / 1000.0
        let rotSpeedAdjusted = timeElapsed * rotSpeedPerMs |> float32

        let rotationMatrix = Matrix3x2.CreateRotation(rotSpeedAdjusted, triangleCenter)

        let speedPerMs = gameState.Speed / 1000.0f
        let speedAdjusted = (timeElapsed |> float32) * speedPerMs

        let newTop = Vector2.Transform(triangle.Top, rotationMatrix)
        let newTopWithSpeed = newTop + speedAdjusted

        let newRight = Vector2.Transform(triangle.Right, rotationMatrix)
        let newRightWithSpeed = newRight + speedAdjusted

        let newLeft = Vector2.Transform(triangle.Left, rotationMatrix)
        let newLeftWithSpeed = newLeft + speedAdjusted

        let isOutOfBoundsX = outOfBoundsTriangleX newLeftWithSpeed newRightWithSpeed newTopWithSpeed
        let isOutOfBoundsY = outOfBoundsTriangleY newLeftWithSpeed newRightWithSpeed newTopWithSpeed
        let isAnyOutOfBounds = isOutOfBoundsX || isOutOfBoundsY
        let newTriangle = 
            if isAnyOutOfBounds then
                { 
                    Top = newTop
                    Left = newLeft
                    Right = newRight
                }
            else 
                {
                    Top =  newTopWithSpeed
                    Left = newLeftWithSpeed
                    Right = newRightWithSpeed
                }

        let newSpeed = 
            if isOutOfBoundsX 
            then Vector2(-gameState.Speed.X, gameState.Speed.Y)
            else if isOutOfBoundsY then Vector2(gameState.Speed.X, -gameState.Speed.Y)
            else gameState.Speed


        { gameState with Triangle = newTriangle; Speed = newSpeed }
