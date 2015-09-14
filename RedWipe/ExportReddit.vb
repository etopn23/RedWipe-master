Imports System.Xml
Imports System


Public Class ExportReddit
    'This class will be used to save the reddit data.
    'Should save the exported XML data in different folders by username
    'Each XML file should be appended with the newest exported data
    'Format Below
    '    <?xml version="1.0" encoding="ISO-8859-1"?>
    '       <Posts>
    '           <post date="12/01/2014">
    '               <title>Empire Burlesque</title>
    '               <artist>Bob Dylan</artist>
    '               <price>10.90</price>
    '           </post>
    '       </Posts>

    Function SaveData(ByVal User As RedditSharp.Things.AuthenticatedUser, Optional ByVal Post As RedditSharp.Things.Post = Nothing, Optional ByVal comment As RedditSharp.Things.Comment = Nothing) As Boolean
        'Function returns true is data successfully saved, otherwise false.
        'Use default save location or require the user to select a folder.
        Dim myDocuments As String = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        Dim basePath As String = myDocuments & "\RedWipe" & "\" & User.Name
        'Check and see if this directory exists
        'If not... create it
        If (Not System.IO.Directory.Exists(basePath)) Then
            System.IO.Directory.CreateDirectory(basePath)
        End If
        'Attempt to Save Date
        Try
            'Save instructions go here.
            If Post IsNot Nothing Then
                'This SaveData is for a post
                Dim filename As String = basePath & "\posts.xml"
                Dim Writer As XmlTextWriter = New XmlTextWriter(filename, System.Text.Encoding.UTF8)
                MsgBox(filename)

                Writer.WriteStartElement("posts")
                Writer.WriteStartElement("post")
                Writer.WriteElementString("title", Post.Title)
                Writer.WriteElementString("CommentCount", Post.CommentCount)
                Writer.WriteElementString("Date", Post.Created)
                Writer.WriteElementString("Downvotes", Post.Downvotes)
                Writer.WriteElementString("Upvotes", Post.Upvotes)
                Writer.WriteElementString("Text", Post.SelfText)
                Writer.WriteElementString("HTMLtext", Convert.ToBase64String(Text.Encoding.ASCII.GetBytes(Post.SelfTextHtml)))
                Writer.WriteElementString("Score", Post.Score)
                Writer.WriteElementString("SaveDate", Date.Now)
                Writer.WriteElementString("URL", Post.Permalink.AbsolutePath)
                Writer.WriteElementString("Id", Post.Id)
                Writer.WriteElementString("NSFW", Post.NSFW)
                Writer.WriteEndElement()
                Writer.WriteEndElement()
                Writer.Flush()
                Writer.Close()
            Else
                'This save data is for a comment
                Dim filename As String = basePath & "\comments.xml"
                MsgBox(filename)
                Dim Writer As XmlTextWriter = New XmlTextWriter(filename, System.Text.Encoding.UTF8)
                Writer.WriteStartElement("comments")
                Writer.WriteStartElement("comment")
                Writer.WriteElementString("Date", comment.Created)
                Writer.WriteElementString("Downvotes", comment.Downvotes)
                Writer.WriteElementString("Upvotes", comment.Upvotes)
                Writer.WriteElementString("Text", comment.Body.ToString)
                Writer.WriteElementString("HTMLtext", Convert.ToBase64String(Text.Encoding.ASCII.GetBytes(comment.BodyHtml)))
                Writer.WriteElementString("Gilded", comment.Gilded)
                Writer.WriteElementString("SaveDate", Date.Now)
                Writer.WriteElementString("URL", comment.Shortlink)
                Writer.WriteElementString("Id", comment.Id)
                Writer.WriteEndElement()
                Writer.WriteEndElement()
                Writer.Flush()
                Writer.Close()
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
