using System;

public enum StatusCode
    {
        UserNotFound = 0,
        UserAlreadyExists = 1,

        GoodNotFound = 10,

        OrderNotFound = 20,

        BasketNotFound = 30,
        ProfileNotFound = 40,

        OK = 200,
        InternalServerError = 500
    }

