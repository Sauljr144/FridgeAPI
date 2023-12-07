Controllers // Folders
    ShoppingItemController / File
        Add food items // endpoint C
        Get all food items // endpoint R
        Get food items by category
        Get food items by exp date
        Update food items // endpoint U
        Delete food items // endpoint D
        Delete all food items // endpoint D

    FridgeItemController / File
        Add food items // endpoint C
        Get all food items // endpoint R
        Get food items by category
        Get food items by exp date
        Update food items // endpoint U
        Delete food items // endpoint D
        Delete all food items // endpoint D


Model // Folder
    FoodItemModel
        string Item
        int ID
        string Description
        string Date
        string Category
        bool IsDeleted


--------

LoginModel
    string Username
    string Password
CreateAccountModelDTO
    int Id = 0
    string Username
    string Password
passwordModelDTO
    string Salt
    string Hash

Services//Folder
    Context//Folder

UserService//file
        GetUsersByUsername
        Login
        Add User
        Delete User
    BlogItemService//file
        Add blog items // functions C
        GetAllBlogItems // functions R
        GetBlogItemsByCategory
        GetBlogItemsByTags
        GetBlogItemsByDate
        UpdateBlogItems // functions U
        DeleteBlogItems // functions D
        GetUserById // functions

PasswordService//file
    Hash Password
    Verify Hash Password