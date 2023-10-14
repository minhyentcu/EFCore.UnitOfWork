# UnitOfWork.EntityFrameworkCore

A plugin for Microsoft.EntityFrameworkCore to support repository, unit of work patterns, and multiple database with distributed transaction supported.


## Installation
```c
Install-Package UnitOfWork.EntityFrameworkCore -Version 5.3.1 (.NET5)
or
Install-Package UnitOfWork.EntityFrameworkCore -Version 6.0.3 (.NET6)
or
Install-Package UnitOfWork.EntityFrameworkCore -Version 7.0.2 (.NET7)
```


## Usage
The following code demonstrates basic usage.

First of all, please register the dependencies into the MS Built-In container:
```csharp
services.AddDbContext<ExampleDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.MainConnectionString), sqlServerOptions =>
                {
                    sqlServerOptions.MigrationsAssembly((typeof(ExampleDbContext).Assembly).GetName().Name);
                });
            }).AddUnitOfWork<ExampleDbContext>();
```
After that, use the structure in your code like that:
```csharp
private readonly IUnitOfWork _unitOfWork;
// Injection
 public ExampleController(IUnitOfWork<ExampleDbContext> unitOfWork )
        {
            _unitOfWork = unitOfWork;
        }
```
#returns GetAll
```csharp
public async Task GetAll(){
  var books= _unitOfWork.GetRepository<Book>().GetAll();
}
```
# returns Get ByFilter
```csharp
public async Task GetAllByFilter(){
  var blogs = _unitOfWork.GetRepository<Book>().GetAllByFilter(
                predicate: x => x.Descriptions.Contains("Book Example"),
                orderBy: x => x.OrderByDescending(i => i.DateCreated),
                include: x => x.Include(i => i.Category));
}
```
# returns GetPaging
```csharp
public async Task GetAllByFilter(){
   var blogs = _unitOfWork.GetRepository<Blog>().GetPagedListAsync(
               predicate: x => x.Descriptions.Contains("Book Example"),
               orderBy: x => x.OrderByDescending(i => i.DateCreated),
               include: x => x.Include(i => i.Category),
               pageIndex:1,pageSize:20);
}
```
# returns GetStagesPaging
```csharp
public async Task GetStagesPaging(){
   var repository = _unitOfWork.GetRepository<Book>();
   var query = repository.Queryable()
                .Where(x => x.Descriptions.Contains("Book Example"));
   var books= _unitOfWork.GetRepository<Book>().GetStagesPagedListAsync(
               stages: query,
               selector:x=>x.Descriptions,
               pageIndex: 1, pageSize: 20);
}
```
# returns GetBookById
```csharp
public async Task voidGetBookById(int id){
  var book= await _unitOfWork.GetRepository<Book>().FindAsync(id);
}
```
# returns AddBook
```csharp
public async Task AddBook(Book book){
    await _unitOfWork.GetRepository<Book>().InsertAsync(book);
    await _unitOfWork.SaveChangesAsync();
}
```
# returns UpdateBook
```csharp
public async Task UpdateBook(Book book){
    _unitOfWork.GetRepository<Book>().Update(book);
    await _unitOfWork.SaveChangesAsync();
}
```
# returns DeleteBook
```csharp
public async Task DeleteBook(int id){
    _unitOfWork.GetRepository<Book>().Delete(id);
    await _unitOfWork.SaveChangesAsync();
}
```
# returns GetBookCount
```csharp
public async Task GetBookCount(){
   var count= _unitOfWork.GetRepository<Book>().CountAsync(id);
}
```
......................
```
The operations above are also available as async.
```
## Support / Contributing

If you want to help with the project, feel free to open pull requests and submit issues.

https://github.com/minhyentcu/EFCore.UnitOfWork

