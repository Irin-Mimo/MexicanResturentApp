#🌮 Mexican Restaurant E-Commerce Web Application


##🚀 Overview

This is a Mexican Restaurant E-Commerce web app built with ASP.NET Core MVC and Entity Framework Core.

Users can browse menu items, add them to a shopping cart, and place orders. Admins can manage products, ingredients, and categories.

The project demonstrates one-to-many and many-to-many relationships, along with ASP.NET Identity for user authentication.

🎯 Features
-👤User Features

-✅ Register & Login using ASP.NET Identity
-✅ Browse menu items by category
-✅ Add products to shopping cart
-✅ Update quantity and remove items
-✅ Place orders and view order history
-✅ Optional: Leave reviews for products

##Admin Features

-✅ Manage Products (Add/Edit/Delete)
-✅ Manage Ingredients & Categories
-✅ Manage product-ingredient relationships (many-to-many)
-✅ View all orders

##Technical Features
-🔗 Use of **ASP.NET Core MVC** with Razor Pages

-Entity Framework Core database management

-Many-to-many: Product ↔ Ingredient

-One-to-many:

-Category → Products
-User → Orders
-Order → OrderItems
-Session-based shopping cart
-Image upload for products
-Responsive UI with Bootstrap

👩‍💻Coder

**Irin Sarker Mim**
* GitHub: [@Irin-Mimo](https://github.com/Irin-Mimo)
* LinkedIn: [Irin Sarker Mim](https://www.linkedin.com/in/irin-sarker-mim/)

##⚡ Getting Started

1. Clone the repository:
 git clone https://github.com/Irin-Mimo/MexicanResturentApp.git
2.Restore NuGet packages
3.Configure the connection string in appsettings.json
4.Apply migrations
5.dotnet ef database update
6.Run the project

##🛠 Tools & Technologies

⚡ ASP.NET Core MVC
🔑 ASP.NET Identity
🗄️ Entity Framework Core
🛢️ SQL Server
🎨 Bootstrap 5
🖥️ Visual Studio 2022
