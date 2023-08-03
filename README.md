![](https://cdn.discordapp.com/attachments/715542309588172824/1017800062602051624/bazarsodailogo.png)
# BazarSodai



## Prerequirements

* Visual Studio 2022
* .NET Core SDK
* SQL Server

## How To Run

* Open solution in Visual Studio 2022
* Set .Web project as Startup Project and build the project.
* Run the application.
* Import the ShopDatabse.bacpac file in Microsoft Sql Server Management Studio
  Right click on Database Folder -> Tasks -> Import Data-tier Application -> Select the ShopDatabase.bacpac file

## Overview 
  
- `Content/*` - It contains all css files for both user and admin interface.
- `Script/*` - It has all Js files which are required to run the cshtml files. 
- `Model/*` - It contains all auto-generated files from database including context, diagram and validation codes. 
- `Controller/*` - It has all the required codes in HomeController which communicates with the cshtml files. 
- `Views/AdminView*` - It has all the cshtml files which generates the admin part of the website.
- `Views/Home*` - It has all the cshtml files which generates the user part of the website.
- `View/shared` - Generally it contains the shared resources like header and footer , error page.
- `scss/*` - Contains all scss files.
- `images/*` - Contains all required image for the website.

Detailed Report
------------


## 1. Introduction
Bazar Sodai is a marketplace to discover, buy and sell a wide variety of items related to food, household,
medicine, and other daily essentials that customers can look for and purchase through this online super shop.
They can easily buy groceries and essential products from home through the internet. The system decreases a
much of workload for the customer. The product is delivered directly to the customer’s address through online
grocery shopping. this project provides a lot of features to manage the product in well manner. This project
contains details advanced modules that can make the back end system very powerful.
### 1.1 Proposed Goals
#### Objective
• Manage the activities in a supermarket and monitor stock information.
• Keep track of the sales made during a specific month.
• Shorten the time required to calculate sales activity.
• Save a lot of data in the database, which will make things less clumsy .
• To lessen the amount of paperwork so that customers can save time.
• This will have an influence on the lives of working people who, due to job pressure, don’t have the time
to attend marketplaces to buy their household goods and other necessities.
#### Motivation
The structure is very convenient for customers. Online Super shop is a method of E-Commerce that allows
customers to buy a product from a seller over the internet. In recent decades, there had been an increase in
demand for e-commerce websites. Super shops in particular are becoming more and more popular as online
grocers. Nowadays people are habituated to using online solutions for every single need. There are now websites
and apps for every daily necessity. That’s why we thought about making this website that will sell almost every
groceries item and medicine. Although this type of solution is already available in the market either they are
not that user-friendly or not that attractive to the target market.
### 1.2 Project Accomplishment
As we have proposed some features before, we’ve tried to implement all of them like(signup, login, product add,
view, show details, add to cart, checkout, transaction, admin management, etc). We proposed one extra feature
which is a search for products but we’ve not implemented that feature. In this short time, we’ve done so far
we are quite satisfied.

## 2 Project Features
### Implemented Features
• SignUp
• Login
• Add Product, Add Category, Add Subcategory
• View Category, View Subcategory, View Product
• Product Details
• Add to Cart
• Checkout
• Order History
• Customer Management
• Product Management(Add,Delete)

### Proposed Features
• SignUp
• Login
• Customer Management
• Product Management (add view product category, subcategory, show product details)
• Delivery Management (place order, purchase history, order status)
• Admin Management
• Add to Cart
• Checkout
• Order History
• Search Product
#### 3 Database Diagram
![](https://cdn.discordapp.com/attachments/715542309588172824/1017799251331383346/finalErd.png)

#### Limitations
• OderDetails is not created
• Not normalized

## 4 Contribution
** Maisha Musarrat Nabila(ID: 190104058)**
• User Login
• User Registration
• Authentication in both User and Admin Panel
• Overall Ui

**Debopriya Deb Roy(ID: 190104065)**
• Project supervision
• Database maintainance and bug management
• Review feature added
• Cart page, Checkout page data fetch and completed
• Hoempage, Category, Subcategory, Product Details data added and fetched
• Routing from different pages
• Database Designed and Created
• Ui Fixes and small improvements and consistancy maintainance

**Israt Moyeen Noumi(ID: 190104070)**
• Admin panel Database Designed and implemented
• Add Category, View Category page implementd with Add and Delete
• Add SubCategory, View SubCategory page implementd with Add and Delete
• Add Products, View Products page implementd with Add and Delete
• Admin Panel Design
• Admin panel Create Acount, Login implementd

## 5 Conclusion
Technology has made significant progress over the years to provide consumers a better online shopping experience
and will continue to do so for years to come. With the rapid growth of products and brands, people have
speculated that online shopping will overtake in-store shopping.The availability of online shopping has produced
a more educated consumer that can shop around with relative ease without having to spend a large amount of
time. At the end, our system has been make a win-win situation for both consumer and sellers.
### 5.1 Limitations
• Admin cannot edit the data
• Searching products feature not implemented
• Numerous exceptions not handled
• Add to favourites not implemented
5CSE3200: Software Development-V Project Report
### 5.2 Future Work
• Sales Management
• Delivery Management with different payment method
• Notification system for users
### 5.3 Project Link
Github : https://github.com/DDR13GIT/BazarSodai
### 5.4 Ending Remarks
Software development projects always help students to gain some exciting knowledge about new software development structures. This software development project has helped us to learn MVC framework, specially
bindings between Model, view and controller. We hope this knowledge will help us in future projects.
